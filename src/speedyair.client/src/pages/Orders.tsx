import { useEffect, useState } from "react";
import Box from '@mui/material/Box';
import { GridColDef } from '@mui/x-data-grid';

import OrdersService from "../api/ordersService";
import Order from "../models/Order";
import OrdersGrid from "../components/OrdersGrid";

const columns: GridColDef[] = [
    { field: 'orderId', headerName: 'Order', width: 200 },
    { field: 'flightId', headerName: 'Flight number', width: 250 },
    { field: 'departure', headerName: 'Departure', width: 250 },
    { field: 'arrival', headerName: 'Arrival', width: 250 },
    { field: 'day', headerName: 'Day', width: 200 },
];

function Orders(): JSX.Element {
    const [orders, setOrders] = useState<Order[]>([]);

    useEffect(() => {
        let disposed = false;
        (async () => {
            const service = new OrdersService("http://localhost:5177");
            const result = await service.getOrdersAsync();
            if (!disposed) {
                setOrders(result);
            }
        })();

        return () => {
            disposed = true;
        };
    }, []);

    return (<>
        <Box className="table-title" sx={{ py: 3, pl: 1, fontSize: 18, fontWeight: "" }}>Orders</Box>
        <OrdersGrid orders={orders} columns={columns} />
    </>);
}

export default Orders;