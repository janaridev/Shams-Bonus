import React from "react";
import Navbar from "../navbar";
import BonusWidget from "../widgets/BonusWidget";
import AdminWidget from "../widgets/AdminWidget";
import { Box, useMediaQuery } from "@mui/material";
import { useSelector } from "react-redux";
import jwtDecode from "jwt-decode";

const HomePage = () => {
  const isNonMobile = useMediaQuery("(min-width:600px)");

  const token = useSelector((state) => state.token);
  const decodedToken = jwtDecode(token);
  const userRole =
    decodedToken[
      "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    ];

  return (
    <Box>
      <Navbar />
      {userRole === "Administrator" ? (
        <Box>
          {isNonMobile ? (
            // DESKTOP
            <Box
              sx={{
                height: "90vh",
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
              }}
            >
              <Box sx={{ width: "100%" }}>
                <AdminWidget />
              </Box>
            </Box>
          ) : (
            // MOBILE
            <Box
              sx={{
                maxWidth: "700px",
                m: "auto",
                height: "90vh",
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
              }}
            >
              <AdminWidget />
            </Box>
          )}
        </Box>
      ) : (
        <Box
          width="70%"
          padding="2rem 6%"
          textAlign="center"
          sx={{
            maxWidth: "700px",
            m: "auto",
            height: "75vh",
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
          }}
        >
          <BonusWidget />
        </Box>
      )}
    </Box>
  );
};

export default HomePage;
