import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-route-form',
  templateUrl: './route-form.component.html',
  styleUrls: ['./route-form.component.css']
})
export class RouteFormComponent implements OnInit {
  routeForm: FormGroup = new FormGroup({});
  isEditMode: boolean = false;
  routeId: string ='';

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private apiService: ApiService
  ) { }

  ngOnInit(): void {
    this.routeForm = this.formBuilder.group({
      origin: ['', Validators.required],
      destination: ['', Validators.required],
      value: ['', Validators.required]
    });

    // Verificar se estamos editando uma rota existente
    this.routeId = this.route.snapshot.paramMap.get('id') ?? '';

    if (this.routeId) {
      this.isEditMode = true;
      this.apiService.getRouteById(this.routeId).subscribe(
        (route) => {
          this.routeForm.patchValue({
            origin: route.origin,
            destination: route.destination,
            value: route.value
          });
        },
        (error) => {
          console.error('Error loading route:', error);
        }
      );
    }
  }

  onSubmit(): void {
    if (this.routeForm.valid) {
      const formData = this.routeForm.value;
      if (this.isEditMode) {
        // Atualizar rota existente
        this.apiService.updateRoute(this.routeId, formData).subscribe(
          () => {
            console.log('Route updated successfully');
            this.router.navigate(['/routes']);
          },
          (error) => {
            console.error('Error updating route:', error);
          }
        );
      } else {
        // Adicionar nova rota
        this.apiService.addRoute(formData).subscribe(
          () => {
            console.log('Route added successfully');
            this.router.navigate(['/routes']);
          },
          (error) => {
            console.error('Error adding route:', error);
          }
        );
      }
    }
  }

}
