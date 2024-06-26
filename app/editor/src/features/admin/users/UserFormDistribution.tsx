import { useFormikContext } from 'formik';
import React from 'react';
import { FaSearch } from 'react-icons/fa';
import { FaPlus, FaTrash } from 'react-icons/fa6';
import { useUsers } from 'store/hooks/admin';
import {
  Button,
  ButtonVariant,
  Col,
  FormikSelect,
  FormikText,
  FormikTextArea,
  getEnumStringOptions,
  IUserEmailModel,
  IUserModel,
  OptionItem,
  Row,
  Section,
  Select,
  Text,
  UserAccountTypeName,
} from 'tno-core';

/**
 * Provides a User Form to manage, create, update and delete a user.
 * @returns React component containing administrative user form.
 */
export const UserFormDistribution: React.FC = () => {
  const { values, setFieldValue } = useFormikContext<IUserModel>();
  const [, { findUsers }] = useUsers();

  const [keyword, setKeyword] = React.useState('');
  const [users, setUsers] = React.useState<IUserModel[]>([]);
  const [selectedUser, setSelectedUser] = React.useState(0);

  const accountTypeOptions = getEnumStringOptions(UserAccountTypeName);
  const userOptions = users.map(
    (u) => new OptionItem(u.preferredEmail ? u.preferredEmail : u.email, u.id),
  );

  const addresses: IUserEmailModel[] = values.preferences?.addresses ?? [];

  React.useEffect(() => {}, []);

  const handleFindUsers = React.useCallback(
    async (keyword: string) => {
      try {
        const results = await findUsers({ keyword });
        setUsers(results.items);
        setSelectedUser(results.items.length ? results.items[0].id : 0);
      } catch {}
    },
    [findUsers],
  );

  return (
    <div className="form-container">
      <Row gap="1rem">
        <Col>
          <FormikSelect
            name="accountType"
            label="Account Type"
            options={accountTypeOptions}
            value={accountTypeOptions.find((s) => s.value === values.accountType) || ''}
            required
            isClearable={false}
          />
          <FormikText
            name="username"
            label="Username"
            required
            onChange={(e) => setFieldValue('username', e.currentTarget.value.toUpperCase())}
          />
          <FormikTextArea name="note" label="Note" />
        </Col>
        <Section className="frm-in distribution-list">
          <Row>
            <Text
              name="findUser"
              label="Find User"
              value={keyword}
              onChange={(e) => setKeyword(e.currentTarget.value)}
              onKeyDown={(e) => {
                if (e.code === 'Enter') {
                  e.preventDefault();
                  handleFindUsers(keyword);
                }
              }}
            />
            <Button title="Search" onClick={() => handleFindUsers(keyword)}>
              <FaSearch />
            </Button>
          </Row>
          <Select
            name="users"
            options={userOptions}
            value={userOptions.find((o) => o.value === selectedUser) ?? ''}
            onChange={(e) => {
              const option = e as OptionItem;
              if (option && option.value) setSelectedUser(+option.value);
              else setSelectedUser(0);
            }}
          >
            <Button
              title="Add"
              onClick={(e) => {
                const user = users.find((u) => u.id === selectedUser);
                const found = user && addresses.some((a) => a.userId === user.id);
                if (user && !found) {
                  const email = user.preferredEmail ? user.preferredEmail : user.email;
                  setFieldValue('preferences', {
                    ...values.preferences,
                    addresses: [{ userId: user.id, email: email }, ...addresses],
                  });
                }
                setSelectedUser(0);
              }}
            >
              <FaPlus />
            </Button>
          </Select>
          <label>Email Addresses</label>
          <Section className="addresses">
            <Col gap="0.5rem">
              {addresses.map((address) => {
                return (
                  <Row key={address.userId} gap="0.5rem" alignItems="center">
                    <Col flex="1">{address.email}</Col>
                    <Button
                      title="Remove"
                      variant={ButtonVariant.red}
                      onClick={(e) => {
                        setFieldValue('preferences', {
                          ...values.preferences,
                          addresses: addresses.filter((a) => a.userId !== address.userId),
                        });
                      }}
                    >
                      <FaTrash />
                    </Button>
                  </Row>
                );
              })}
            </Col>
          </Section>
        </Section>
      </Row>
    </div>
  );
};
