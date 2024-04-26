import { useEffect, useState } from "react";
import { NavigateFunction, useNavigate } from "react-router-dom";
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';

import ScheduleService from "../api/scheduleService";
import Flight from "../models/Flight";
import { flightPagePath } from "../common/pathNames";

function getGridColumns(navigate: NavigateFunction) {
    const columns: GridColDef[] = [
        { field: 'flightId', headerName: 'Flight number', width: 200 },
        { field: 'departure', headerName: 'Departure', width: 250 },
        { field: 'arrival', headerName: 'Arrival', width: 250 },
        {
            field: 'action', headerName: 'View flight', width: 200, renderCell: (params) => {
                const onClick = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
                    e.stopPropagation();
                    navigate(`${flightPagePath}${params.row.flightId}`)
                };

                return <Button variant="contained" onClick={onClick}>View flight</Button>;
            }
        }
    ];
    return columns;
}

function Schedule(): JSX.Element {
    const [schedule, setSchedule] = useState<Flight[]>([]);
    const navigate = useNavigate();
    const columns = getGridColumns(navigate);

    useEffect(() => {
        let disposed = false;
        (async () => {
            const service = new ScheduleService("http://localhost:5177");
            const schedule = await service.getScheduleAsync();
            if (!disposed) {
                setSchedule(schedule);
            }
        })();

        return () => {
            disposed = true;
        };
    }, []);

    const flightGroupsByDay = schedule.reduce((days: Map<number, Flight[]>, flight: Flight) => {
        if (!days.has(flight.day)) {
            days.set(flight.day, [flight])
        } else {
            days.get(flight.day)?.push(flight);
        }
        return days;
    }, new Map<number, Flight[]>);

    return (
        <div>
            {
                [...flightGroupsByDay.entries()].map((day: [number, Flight[]]) => {
                    return (
                        <Box sx={{ p: 3 }}>
                            <Box sx={{pb: 2}}>Schedule flights for day {day[0]}</Box>
                            <DataGrid
                                rows={day[1]}
                                columns={columns}
                                pageSizeOptions={[5, 10]}
                            />
                        </Box>
                    );
                })
            }
        </div>
    );
}

export default Schedule;