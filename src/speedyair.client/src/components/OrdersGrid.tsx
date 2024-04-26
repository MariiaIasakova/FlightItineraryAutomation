import { DataGrid, GridColDef } from '@mui/x-data-grid';
import Order from "../models/Order";

interface OrdersGridProps{
    orders: Order[],
    columns: GridColDef[],
}

const OrdersGrid: React.FC<OrdersGridProps> = ({ orders, columns }) => {
    return (<DataGrid
        rows={orders}
        columns={columns}
        pageSizeOptions={[5, 10]}
        initialState={{
            pagination: {
                paginationModel: { page: 0, pageSize: 10 },
            },
        }}
    />);
}

export default OrdersGrid;