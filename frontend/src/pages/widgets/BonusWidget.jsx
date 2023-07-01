import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import WidgetWrapper from "../../components/WidgetWrapper";
import { Box, Typography, useMediaQuery } from "@mui/material";
import axios from "axios";
import { toast } from "react-toastify";

const BonusWidget = () => {
  const [bonuses, setBonuses] = useState();
  const token = useSelector((state) => state.token);
  const theme = useSelector((state) => state.mode);
  const isNonMobile = useMediaQuery("(min-width:600px)");

  useEffect(() => {
    getBonuses();
  }, []);

  const getBonuses = async () => {
    try {
      const response = await axios.get("http://localhost:5000/api/user", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      if (response.status === 200) {
        setBonuses(response.data.result);
      }
    } catch (error) {
      toast.error(`${error.response.data.errorMessages}`, {
        position: "top-right",
        autoClose: 2000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: theme === "light" ? "light" : "dark",
      });
    }
  };

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
