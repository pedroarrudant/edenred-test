import { Routes } from '@angular/router';
import { ClientList } from './pages/client-list/client-list';
import { NewClient } from './pages/new-client/new-client';

export const routes: Routes = [
    { path: 'new-client', component: NewClient},
    { path: 'client-list', component: ClientList},
];
