import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject, signal } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { environment } from '../../../environments/environment';
import { Vehicle } from '../../models/vehicle';

@Component({
  imports: [MatTableModule],
  templateUrl: './vehicle-list.html',
})
export class VehicleList implements OnInit {
  private readonly http = inject(HttpClient);

  vehicles = signal<Vehicle[]>([]);
  isLoading = signal(true);
  errorMessage = signal('');

  displayedColumns = ['licensePlate', 'manufactureYear', 'brand', 'model'];

  ngOnInit(): void {
    this.http.get<Vehicle[]>(`${environment.apiUrl}/vehicles`).subscribe({
      next: (vehicles) => {
        this.vehicles.set(vehicles);
        this.isLoading.set(false);
      },
      error: () => {
        this.errorMessage.set('Could not load vehicles.');
        this.isLoading.set(false);
      },
    });
  }
}
