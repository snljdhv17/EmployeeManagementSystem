import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule],
  selector: 'app-login',
  styleUrl: './login.css',
  templateUrl: './login.html',
})
export class Login {
  
  loginObj: any={
    email:'',
    contactNo:''
  }
   loginResult = signal<any>(null);
  errorMessage = signal<string>('');

  http = inject(HttpClient);
  router = inject(Router);

  onLogin(event: Event){
  
  event.preventDefault();
  this.http.post('./../api/EmployeeMaster/login', this.loginObj).subscribe({
  next:(result:any)=>{
     console.log("Login Response:", result);

    const userData = JSON.stringify(result.data);

    // Save to localStorage
    localStorage.setItem("emploginUser", userData);

    // Verify whether it was saved
    console.log("Saved User:", localStorage.getItem("emploginUser"));

    // localStorage.setItem("emploginUser", JSON.stringify(result.data));
    // console.log(JSON.stringify(result.data));
    if(result.data.role == "Employee"){
     this.router.navigateByUrl("new-employee/"+ result.data.employeeId);
    }else {
    this.router.navigateByUrl("dashboard");
    }
  
  },
  error:(err: any)=>{

  }
  })
  }

}
