import React from "react";
import { useSelector } from "react-redux";
import WidgetWrapper from "../../components/WidgetWrapper";
import { Box, Typography, useMediaQuery } from "@mui/material";

const BonusWidget = () => {
  const bonuses = useSelector((state) => state.user.bonuses);
  const isNonMobile = useMediaQuery("(min-width:600px)");

  return (
    <WidgetWrapper>
      <Box
        sx={{
          fontSize: "40px",
          fontWeight: "500",
          p: isNonMobile ? "5rem" : "2.5rem",
        }}
      >
        У вас{" "}
        <Typography
          fontWeight="700"
          fontSize="clamp(1rem, 2rem, 2.25rem)"
          color="primary"
          sx={{
            fontSize: isNonMobile ? "60px" : "50px",
            textDecoration: "underline",
          }}
        >
          {bonuses}
        </Typography>
        бонусов
      </Box>
    </WidgetWrapper>
  );
};

export default BonusWidget;
