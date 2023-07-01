import { useState } from "react";
import {
  Box,
  Button,
  TextField,
  useMediaQuery,
  Typography,
  useTheme,
} from "@mui/material";
import { Formik } from "formik";
import * as yup from "yup";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { setLogin } from "../../state";
import axios from "axios";
import { toast } from "react-toastify";

const registerSchema = yup.object().shape({
  firstName: yup.string().max(15, "Максимум 15 символов."),
  lastName: yup.string().max(15, "Максимум 15 символов."),
  userName: yup //will be phone number
    .string()
    .min(10, "Вы ввели не правильный номер телефона.")
    .max(10, "Вы ввели не правильный номер телефона.")
    .required("Обязательное поле."),
  password: yup
    .string()
    .min(7, "Пароль должен содержать не менее 7 символов, и 1 цифру")
    .max(20, "Пароль должен содержать не более 20 символов.")
    .required("Обязательное поле."),
});

const loginSchema = yup.object().shape({
  userName: yup.string().required("Обязательное поле."),
  password: yup.string().required("Обязательное поле."),
});

const initialValuesRegister = {
  firstName: "",
  lastName: "",
  userName: "",
  password: "",
  confirmPassword: "",
};

const initialValuesLogin = {
  userName: "",
  password: "",
};

const Form = () => {
  const [pageType, setPageType] = useState("login");
  const [error, setError] = useState(null);
  const { palette } = useTheme();
  const theme = useSelector((state) => state.mode);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const isNonMobile = useMediaQuery("(min-width:600px)");
  const isLogin = pageType === "login";
  const isRegister = pageType === "register";

  const register = async (values, onSubmitProps) => {
    try {
      const response = await axios.post(
        "http://localhost:5000/api/authentication",
        values
      );
      onSubmitProps.resetForm();

      if (response.status === 200) {
        setPageType("login");
      }
    } catch (error) {
      setError(error.response.data.errorMessages);
    }
  };

  const login = async (values, onSubmitProps) => {
    try {
      const response = await axios.post(
        "http://localhost:5000/api/authentication/login",
        values,
        {
          headers: { "Content-Type": "application/json" },
        }
      );
      const loggedIn = response.data;

      onSubmitProps.resetForm();

      if (loggedIn) {
        dispatch(
          setLogin({
            user: loggedIn.result.user,
            token: loggedIn.result.token,
          })
        );
        toast.success("Добро Пожаловать", {
          position: "top-right",
          autoClose: 2000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
          theme: theme === "light" ? "light" : "dark",
        });
        navigate("/home");
      }
    } catch (error) {
      onSubmitProps.resetForm();
      setError(error.response.data.errorMessages);
    }
  };

  const handleFormSubmit = async (values, onSubmitProps) => {
    if (isLogin) await login(values, onSubmitProps);
    if (isRegister) await register(values, onSubmitProps);
  };

  return (
    <Formik
      onSubmit={handleFormSubmit}
      initialValues={isLogin ? initialValuesLogin : initialValuesRegister}
      validationSchema={isLogin ? loginSchema : registerSchema}
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
          <Box
            display="grid"
            gap="30px"
            gridTemplateColumns="repeat(4, minmax(0, 1fr))"
            sx={{
              "& > div": { gridColumn: isNonMobile ? undefined : "span 4" },
            }}
          >
            {isRegister && (
              <>
                <TextField
                  label="Имя"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  value={values.firstName}
                  name="firstName"
                  error={
                    Boolean(touched.firstName) && Boolean(errors.firstName)
                  }
                  helperText={touched.firstName && errors.firstName}
                  sx={{ gridColumn: "span 2" }}
                />
                <TextField
                  label="Фамилия"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  value={values.lastName}
                  name="lastName"
                  error={Boolean(touched.lastName) && Boolean(errors.lastName)}
                  helperText={touched.lastName && errors.lastName}
                  sx={{ gridColumn: "span 2" }}
                />
                <TextField
                  label="Номер Телефона"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  value={values.userName}
                  name="userName"
                  error={Boolean(touched.userName) && Boolean(errors.userName)}
                  helperText={touched.userName && errors.userName}
                  sx={{ gridColumn: "span 4" }}
                  InputProps={{
                    startAdornment: (
                      <span
                        style={{
                          paddingTop: "1px",
                        }}
                      >
                        +7
                      </span>
                    ),
                  }}
                />
                <TextField
                  label="Пароль"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  value={values.password}
                  name="password"
                  error={Boolean(touched.password) && Boolean(errors.password)}
                  helperText={touched.password && errors.password}
                  sx={{ gridColumn: "span 4" }}
                />
                <TextField
                  label="Подтверждение пароля"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  value={values.confirmPassword}
                  name="confirmPassword"
                  error={Boolean(touched.password) && Boolean(errors.password)}
                  helperText={touched.password && errors.password}
                  sx={{ gridColumn: "span 4" }}
                />
                {error !== null && (
                  <Typography
                    variant="subtitle1"
                    color="error"
                    sx={{
                      whiteSpace: "nowrap",
                      p: 0,
                      m: 0,
                      lineHeight: "0",
                    }}
                  >
                    {error}
                  </Typography>
                )}
              </>
            )}

            {isLogin && (
              <>
                <TextField
                  label="Номер Телефона"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  value={values.userName}
                  name="userName"
                  error={Boolean(touched.userName) && Boolean(errors.userName)}
                  helperText={touched.userName && errors.userName}
                  sx={{ gridColumn: "span 4" }}
                  InputProps={{
                    startAdornment: (
                      <span
                        style={{
                          paddingTop: "2px",
                        }}
                      >
                        +7
                      </span>
                    ),
                  }}
                />
                <TextField
                  label="Пароль"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  value={values.password}
                  name="password"
                  error={Boolean(touched.password) && Boolean(errors.password)}
                  helperText={touched.password && errors.password}
                  sx={{ gridColumn: "span 4" }}
                />
                {error !== null && (
                  <Typography
                    variant="subtitle1"
                    color="error"
                    sx={{
                      whiteSpace: "nowrap",
                      p: 0,
                      m: 0,
                      lineHeight: "0",
                    }}
                  >
                    {error}
                  </Typography>
                )}
              </>
            )}
          </Box>

          {/* BUTTONS */}
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
              {isLogin ? "ВОЙТИ" : "ЗАРЕГЕСТРИРОВАТЬСЯ"}
            </Button>
            <Typography
              onClick={() => {
                setPageType(isLogin ? "register" : "login");
                resetForm();
                setError(null);
              }}
              sx={{
                textDecoration: "underline",
                color: palette.primary.main,
                "&:hover": {
                  cursor: "pointer",
                  color: palette.primary.light,
                },
              }}
            >
              {isLogin
                ? "Нету аккаунта? Зарегестрируйтесь тут!"
                : "Уже есть аккаунт? Войдите тут!"}
            </Typography>
          </Box>
        </form>
      )}
    </Formik>
  );
};

export default Form;
