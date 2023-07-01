import React from "react";
import { Box, useMediaQuery } from "@mui/material";
import AdminWidget from "../widgets/AdminWidget";

const AdminPage = () => {
  const isNonMobile = useMediaQuery("(min-width:600px)");
  return (
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
  );
};

export default AdminPage;
