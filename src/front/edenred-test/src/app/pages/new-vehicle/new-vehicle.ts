import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
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
    clientId: this.formBuilder.control<number | null>(null, [
      Validators.required,
      Validators.min(1),
      Validators.max(Number.MAX_SAFE_INTEGER),
      Validators.pattern(/^\d+$/),
    ]),
    licensePlate: '',
    manufactureYear: this.formBuilder.control<number | null>(null),
    brand: '',
    model: '',
  });

  submit(): void {
    if (this.isSubmitting() || this.form.invalid) {
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
      error: (response: HttpErrorResponse) => {
        this.errorMessage.set(
          response.status === 400 && typeof response.error === 'string'
            ? response.error
            : 'Could not create the vehicle.',
        );
        this.isSubmitting.set(false);
      },
    });
  }
}
