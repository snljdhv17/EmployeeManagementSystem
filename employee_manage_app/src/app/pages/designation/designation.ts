import { Component, inject, OnInit, signal } from '@angular/core';
import { Master } from '../../services/master';
import { DesignationModel } from '../../models/department.model';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, NgForm } from '@angular/forms';
import { NgFor, NgForOf, NgIf } from '@angular/common';

@Component({
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, NgFor, NgForOf],
  selector: 'app-designation',
  styleUrl: './designation.css',
  templateUrl: './designation.html',
})
export class Designation implements OnInit {

  private fb = inject(FormBuilder);
  private designationService = inject(Master);
  private departmentService = inject(Master);

  designationForm!: FormGroup;

  designationList = signal<any[]>([]);
departmentList = signal<any[]>([]);

  isEditMode = false;

  ngOnInit(): void {
    this.initializeForm();
    this.getAllDepartments();
    this.getAllDesignation();
  }

  // Initialize reactive form
  initializeForm(): void {
    this.designationForm = this.fb.group({
      designationId: [0],
      departmentId: ['', Validators.required],
      designationName: ['', [
        Validators.required,
        Validators.maxLength(100)
      ]],
     
    });
  }

  // Get all departments
getAllDepartments(): void {
  this.departmentService.getAllDept().subscribe({
    next: (res: any) => {
      this.departmentList.set(res);
    },
    error: (err) => {
      console.error('Error loading departments:', err);
    }
  });
}

// Get all designations
getAllDesignation(): void {
  this.designationService.getAllDesignation().subscribe({
    next: (res: any) => {
      this.designationList.set(res);
    },
    error: (err) => {
      console.error('Error loading designations:', err);
    }
  });
}

  // Save or update
onSave(): void {
  if (this.designationForm.invalid) {
    this.designationForm.markAllAsTouched();
    return;
  }

  const formValue = this.designationForm.getRawValue();

  const obj: DesignationModel = {
    designationId: Number(formValue.designationId),
    departmentId: Number(formValue.departmentId),
    designationName: formValue.designationName.trim(),

  };

  if (this.isEditMode) {

    // UPDATE designation
    this.designationService.updateDesignation(obj).subscribe({
      next: (res: any) => {
        alert('Designation updated successfully.');

        this.getAllDesignation();
        this.onReset();
      },
      error: (err) => {
        console.error('Update error:', err);

        alert(
          err.error?.message || 'Failed to update designation.'
        );
      }
    });

  } else {

    // ADD designation
    this.designationService.saveDesignation(obj).subscribe({
      next: () => {
        alert('Designation saved successfully.');

        this.getAllDesignation();
        this.onReset();
      },
      error: (err) => {
        console.error('Save error:', err);

        alert(
          err.error?.message || 'Failed to save designation.'
        );
      }
    });
  }
}

  // Edit designation
  onEdit(item: any): void {
    this.designationForm.patchValue({
      designationId: item.designationId,
      departmentId: item.departmentId,
      designationName: item.designationName,
     
    });

    this.isEditMode = true;
  }

  // Delete designation
  onDelete(id: number): void {
    if (!confirm('Are you sure you want to delete this designation?')) {
      return;
    }

    this.designationService.deleteDesignationById(id).subscribe({
      next: () => {
        alert('Designation deleted successfully.');
        this.getAllDesignation();

        if (Number(this.designationForm.value.designationId) === id) {
          this.onReset();
        }
      },
      error: (err) => {
        console.error('Error deleting designation:', err);
        alert('Failed to delete designation.');
      }
    });
  }

  // Reset form
  onReset(): void {
    this.designationForm.reset({
      designationId: 0,
      departmentId: '',
      designationName: '',
   
    });

    this.isEditMode = false;
  }

  // Convenient form control access for validation
  get f() {
    return this.designationForm.controls;
  }
}