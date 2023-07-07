import React from "react";
import { Formik } from "formik";
import { TextField, Button, Box, useTheme, useMediaQuery } from "@mui/material";
import { useSelector } from "react-redux";
import FlexBetween from "../../components/FlexBetween";
import * as yup from "yup";
import axios from "axios";
import { toast } from "react-toastify";

const addBonusesSchema = yup.object().shape({
  phoneNumber: yup
    .string()
    .matches(/^[0-9]+$/, "Введите только цифры.")
    .min(10, "Вы ввели не правильный номер телефона.")
    .max(10, "Вы ввели не правильный номер телефона.")
    .required("Обязательное поле."), // will be phone number
  value: yup
    .number()
    .min(1, "Минимальное число должно быть 1.")
    .required("Обязательное поле."),
});

const initialValuesAddBonuses = {
  phoneNumber: "", // will be phone number
  value: 0,
};

const addBonuses = async (
  values,
  onSubmitProps,
  token,
  setActiveButton,
  theme
) => {
  try {
    const response = await axios.put(
      `http://localhost:5000/api/admin/addBonuses/${values.phoneNumber}`,
      values,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (response.status === 200) {
      setActiveButton(null);
      toast.success("Успешно!", {
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

    onSubmitProps.resetForm();
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

const handleFormSubmit = async (
  values,
  onSubmitProps,
  token,
  setActiveButton,
  theme
) => {
  await addBonuses(values, onSubmitProps, token, setActiveButton, theme);
};

const AddBonuses = ({ setActiveButton }) => {
  const { palette } = useTheme();
  const isNonMobile = useMediaQuery("(min-width:600px)");
  const token = useSelector((state) => state.token);
  const theme = useSelector((state) => state.mode);

  return (
    <Formik
      onSubmit={(values, onSubmitProps) =>
        handleFormSubmit(values, onSubmitProps, token, setActiveButton, theme)
      }
      initialValues={initialValuesAddBonuses}
      validationSchema={addBonusesSchema}
    >
      {({
        values,
        errors,
        touched,
        handleBlur,
        handleChange,
        handleSubmit,
        resetForm,
      }) => (
        <form onSubmit={handleSubmit}>
          <FlexBetween gap="1rem" alignItems="center">
            <Box sx={{ display: "flex", flexDirection: "column", flex: 1 }}>
              <TextField
                label="Номер Телефона"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.phoneNumber}
                name="phoneNumber"
                error={
                  Boolean(touched.phoneNumber) && Boolean(errors.phoneNumber)
                }
                helperText={touched.phoneNumber && errors.phoneNumber}
                sx={{ gridColumn: "span 2" }}
                InputProps={{
                  startAdornment: (
                    <span
                      style={{
                        paddingTop: isNonMobile ? "0px" : "2px",
                      }}
                    >
                      +7
                    </span>
                  ),
                }}
              />
            </Box>
            <Box sx={{ display: "flex", flexDirection: "column", flex: 1 }}>
              <TextField
                label="Сумма чека"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.value}
                name="value"
                error={Boolean(touched.value) && Boolean(errors.value)}
                helperText={touched.value && errors.value}
                sx={{ gridColumn: "span 2" }}
              />
            </Box>
          </FlexBetween>

          <Box>
            <Button
              fullWidth
              type="submit"
              sx={{
                m: "2rem 0",
                p: "1rem",
                backgroundColor: palette.primary.main,
                color: palette.background.alt,
                "&:hover": { color: palette.primary.main },
              }}
            >
              Вычислить
            </Button>
          </Box>
        </form>
      )}
    </Formik>
  );
};

export default AddBonuses;
