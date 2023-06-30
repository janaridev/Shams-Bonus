import React from "react";
import Navbar from "../navbar";
import BonusWidget from "../widgets/BonusWidget";
import { Box } from "@mui/material";

const HomePage = () => {
  return (
    <Box>
      <Navbar />
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
    </Box>
  );
};

export default HomePage;
