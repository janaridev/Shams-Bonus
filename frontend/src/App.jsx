import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import LoginPage from "./pages/loginPage";
import HomePage from "./pages/homePage";
import { useSelector } from "react-redux";
import { useMemo } from "react";
import {
  CssBaseline,
  ThemeProvider,
  createTheme,
  useMediaQuery,
} from "@mui/material";
import { themeSettings } from "./theme";
import { ToastContainer } from "react-toastify";
import "./customStylesToastify.css";
import "react-toastify/dist/ReactToastify.css";

function App() {
  const mode = useSelector((state) => state.mode);
  const theme = useMemo(() => createTheme(themeSettings(mode)), [mode]);
  const isAuth = Boolean(useSelector((state) => state.token));
  const isNonMobile = useMediaQuery("(min-width:600px)");

  return (
    <div className="app">
      <BrowserRouter>
        <ThemeProvider theme={theme}>
          <CssBaseline />
          <Routes>
            <Route path="/" element={<LoginPage />} />
            <Route
              path="/home"
              element={isAuth ? <HomePage /> : <Navigate to="/" />}
            />
          </Routes>
          <ToastContainer toastClassName={isNonMobile ? "" : "mobile-toast"} />
        </ThemeProvider>
      </BrowserRouter>
    </div>
  );
}

export default App;
