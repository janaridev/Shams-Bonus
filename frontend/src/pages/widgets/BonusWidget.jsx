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
          fontSize: "20px",
          fontWeight: "500",
          p: isNonMobile ? "3rem" : "2rem",
        }}
      >
        У вас{" "}
        <Typography
          fontWeight="bold"
          fontSize="clamp(1rem, 2rem, 2.25rem)"
          color="primary"
        >
          {bonuses}
        </Typography>{" "}
        бонусов
      </Box>
    </WidgetWrapper>
  );
};

export default BonusWidget;
