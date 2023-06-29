import React from "react";
import { useSelector } from "react-redux";

const HomePage = () => {
  const { firstName, lastName, userName, bonuses } = useSelector(
    (state) => state.user
  );
  const showedInformation =
    firstName || lastName === null ? userName : `${firstName} ${lastName}`;
  return (
    <>
      {showedInformation} {bonuses}
    </>
  );
};

export default HomePage;
