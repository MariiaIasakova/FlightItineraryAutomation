import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import { useLocation, useNavigate, Outlet } from 'react-router-dom'

import { schedulePagePath, ordersPagePath } from "../common/pathNames";

function Layout(): JSX.Element {
    const location = useLocation();
    const navigate = useNavigate();

    const [text, onClick] = location.pathname === ordersPagePath
        ? ["visit flight schedule", () => navigate(schedulePagePath)]
        : ["visit order schedule", () => navigate(ordersPagePath)];

    return <>
        <Box sx={{ display: 'inline-flex', width: "100%", justifyContent: 'space-between', fontSize: 12 }}>
            <Box sx={{ display: 'inline-flex', alignItems: 'end', }} >
                <Box className="title" sx={{ px: 2, fontSize: 40, color: 'blue' }}>Transport.ly</Box>
                <Box className="sub-title" sx={{ pb: 1 }} >An automated air freight scheduling service.</Box>
            </Box>
            <Button variant="contained" size="small" sx={{ mx: 2 }} onClick={onClick}>{text}</Button>
        </Box>
        <Outlet />
    </>;
}

export default Layout;