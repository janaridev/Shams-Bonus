import React, { useState } from "react";
import WidgetWrapper from "../../components/WidgetWrapper";
import { Box, Button, useMediaQuery, useTheme } from "@mui/material";
import AddBonuses from "../forms/AddBonuses";
import DeductionBonuses from "../forms/DeductionBonuses";

const AdminWidget = () => {
  const [activeButton, setActiveButton] = useState(null);
  const isNonMobile = useMediaQuery("(min-width:600px)");
  const { palette } = useTheme();

  const handleButtonClick = (buttonName) => {
    setActiveButton(buttonName);
  };

  return (
    <WidgetWrapper>
      {activeButton === null && (
        <Box
          sx={{
            display: isNonMobile ? "flex" : "block",
            gap: "3rem",
          }}
        >
          <Button
            fullWidth
            onClick={() => handleButtonClick("add")}
            sx={{
              m: "2rem 0",
              p: "1rem",
              backgroundColor: palette.primary.main,
              color: palette.background.alt,
              "&:hover": { color: palette.primary.main },
            }}
          >
            Пополнение бонусов
          </Button>
          <Button
            fullWidth
            onClick={() => handleButtonClick("deduction")}
            sx={{
              m: "2rem 0",
              p: "1rem",
              backgroundColor: palette.primary.main,
              color: palette.background.alt,
              "&:hover": { color: palette.primary.main },
            }}
          >
            Покупка за бонусы
          </Button>
        </Box>
      )}  

      {activeButton === "add" && (
        <AddBonuses setActiveButton={setActiveButton} />
      )}

      {activeButton === "deduction" && (
        <DeductionBonuses setActiveButton={setActiveButton} />
      )}
    </WidgetWrapper>
  );
};

export default AdminWidget;
