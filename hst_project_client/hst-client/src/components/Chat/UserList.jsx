import PropTypes from 'prop-types';

const UserList = ({ users, onSelectUser }) => {
  // component logic
};

UserList.propTypes = {
  users: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number.isRequired,
      name: PropTypes.string.isRequired,
    })
  ).isRequired,
  onSelectUser: PropTypes.func.isRequired,
};

export default UserList;
