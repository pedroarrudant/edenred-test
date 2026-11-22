import { HttpClient } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { NonNullableFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { environment } from '../../../environments/environment';

@Component({
  imports: [ReactiveFormsModule, MatButtonModule, MatFormFieldModule, MatInputModule],
  templateUrl: './new-vehicle.html',
  styleUrl: './new-vehicle.css',
})
export class NewVehicle {
  private readonly formBuilder = inject(NonNullableFormBuilder);
  private readonly http = inject(HttpClient);

  isSubmitting = signal(false);
  successMessage = signal('');
  errorMessage = signal('');

  form = this.formBuilder.group({
    licensePlate: '',
    manufactureYear: this.formBuilder.control<number | null>(null),
    brand: '',
    model: '',
  });

  submit(): void {
    if (this.isSubmitting()) {
      return;
    }

    this.isSubmitting.set(true);
    this.successMessage.set('');
    this.errorMessage.set('');

    this.http.post<void>(`${environment.apiUrl}/vehicles`, this.form.getRawValue()).subscribe({
      next: () => {
        this.successMessage.set('Vehicle created successfully.');
        this.form.reset();
        this.isSubmitting.set(false);
      },
      error: () => {
        this.errorMessage.set('Could not create the vehicle.');
        this.isSubmitting.set(false);
      },
    });
  }
}
