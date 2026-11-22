import { Routes } from '@angular/router';
import { ClientList } from './pages/client-list/client-list';
import { NewClient } from './pages/new-client/new-client';
import { NewVehicle } from './pages/new-vehicle/new-vehicle';
import { VehicleList } from './pages/vehicle-list/vehicle-list';

export const routes: Routes = [
    { path: 'new-client', component: NewClient},
    { path: 'client-list', component: ClientList},
    { path: 'new-vehicle', component: NewVehicle},
    { path: 'vehicle-list', component: VehicleList},
];
