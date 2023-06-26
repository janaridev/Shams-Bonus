import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import LoginPage from "./pages/loginPage";
import HomePage from "./pages/homePage";
import AdminPage from "./pages/adminPage";
import { useSelector } from "react-redux";
import { useMemo } from "react";
import { CssBaseline, ThemeProvider, createTheme } from "@mui/material";
import { themeSettings } from "./theme";

function App() {
  const mode = useSelector((state) => state.mode);
  const theme = useMemo(() => createTheme(themeSettings(mode)), [mode]);
  const isAuth = Boolean(useSelector((state) => state.token));

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
            <Route
              path="/admin"
              element={isAuth ? <AdminPage /> : <Navigate to="/" />}
            />
          </Routes>
        </ThemeProvider>
      </BrowserRouter>
    </div>
  );
}

export default App;
