import { Component, OnInit, inject, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { Client } from '../../models/client';
import { ClientsService } from '../../services/clients-service';

@Component({
  selector: 'app-client-list',
  imports: [DatePipe, MatTableModule],
  templateUrl: './client-list.html',
  styleUrl: './client-list.css',
})
export class ClientList implements OnInit {
  private readonly clientsService = inject(ClientsService);

  clients = signal<Client[]>([]);
  isLoading = signal(true);
  errorMessage = signal('');

  displayedColumns = [
    'fullName',
    'identificationDocument',
    'birthDate',
    'email',
    'telephoneNumber',
  ];

  ngOnInit(): void {
    this.clientsService.getAll().subscribe({
      next: (clients) => {
        console.log('Clients response:', clients);
        this.clients.set(clients);
        this.isLoading.set(false);
      },
      error: (error) => {
        console.error('Could not load clients:', error);
        this.errorMessage.set('Could not load clients.');
        this.isLoading.set(false);
      },
    });
  }
}
