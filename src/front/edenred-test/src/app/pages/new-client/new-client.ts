import { Component, inject } from '@angular/core';
import {
  NonNullableFormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ClientsService } from '../../services/clients-service';

@Component({
  imports: [
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  templateUrl: './new-client.html',
  styleUrl: './new-client.css',
})
export class NewClient {
  private readonly formBuilder = inject(NonNullableFormBuilder);
  private readonly clientsService = inject(ClientsService);

  isSubmitting = false;
  successMessage = '';
  errorMessage = '';

  form = this.formBuilder.group({
    fullName: ['', Validators.required],
    identificationDocument: ['', Validators.required],
    birthDate: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    telephoneNumber: ['', Validators.required],
  });

  submit(): void {
    this.form.markAllAsTouched();

    if (this.form.invalid || this.isSubmitting) {
      return;
    }

    this.isSubmitting = true;
    this.successMessage = '';
    this.errorMessage = '';

    this.clientsService.create(this.form.getRawValue()).subscribe({
      next: () => {
        this.successMessage = 'Client created successfully.';
        this.form.reset();
        this.isSubmitting = false;
      },
      error: () => {
        this.errorMessage = 'Could not create the client.';
        this.isSubmitting = false;
      },
    });
  }
}
