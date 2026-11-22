import { Component, inject, signal } from '@angular/core';
import {
  FormGroupDirective,
  NonNullableFormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ClientsService } from '../../services/clients-service';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';

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
  private readonly http = inject(HttpClient);

  isSubmitting = signal(false);
  successMessage = signal('');
  errorMessage = signal('');

  form = this.formBuilder.group({
    fullName: ['', Validators.required],
    identificationDocument: ['', Validators.required],
    birthDate: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    telephoneNumber: ['', Validators.required],
  });

  submit(formDirective: FormGroupDirective): void {
    if (this.isSubmitting()) {
      return;
    }

    this.isSubmitting.set(true);
    this.successMessage.set('');
    this.errorMessage.set('');

    this.http.post<void>(`${environment.apiUrl}/clients`, this.form.getRawValue()).subscribe({
      next: () => {
        this.successMessage.set('Client created successfully.');
        formDirective.resetForm();
        this.isSubmitting.set(false);
      },
      error: () => {
        this.errorMessage.set('Could not create the client.');
        this.isSubmitting.set(false);
      },
    });
  }
}
