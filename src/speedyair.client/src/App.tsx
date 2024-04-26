import { BrowserRouter, Routes, Route } from "react-router-dom";

import { Orders, OrdersByFlight, Schedule } from "./pages";
import { basePagePath, flightPagePath, ordersPagePath } from "./common/pathNames"
import Layout from "./components/Layout";

function App(): JSX.Element {
    return (<>
        <BrowserRouter>
          <Routes>
                <Route path={basePagePath} element={<Layout />}>
                    <Route index element={<Schedule />} />
                    <Route path={ordersPagePath} element={<Orders />} />
                    <Route path={`${flightPagePath}:flightId`} element={<OrdersByFlight />} />
                </Route>
            </Routes>
        </BrowserRouter>
    </>);
}

export default App;