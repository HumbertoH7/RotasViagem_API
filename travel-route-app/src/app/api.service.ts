import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'http://localhost:3000/api/routes'; // URL da sua API

  constructor(private http: HttpClient) { }

  // Método para lidar com erros
  private handleError(error: any): Observable<never> {
    console.error('An error occurred:', error);
    return throwError(error);
  }

  // Método para obter todas as rotas
  getRoutes(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl)
      .pipe(
        catchError(this.handleError)
      );
  }

  // Método para obter uma rota pelo ID
  getRouteById(routeId: string): Observable<any> {
    const url = `${this.apiUrl}/${routeId}`;
    return this.http.get<any>(url)
      .pipe(
        catchError(this.handleError)
      );
  }

  // Método para adicionar uma nova rota
  addRoute(route: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, route)
      .pipe(
        catchError(this.handleError)
      );
  }

  // Método para atualizar uma rota existente
  updateRoute(routeId: string, updatedRoute: any): Observable<any> {
    const url = `${this.apiUrl}/${routeId}`;
    return this.http.put<any>(url, updatedRoute)
      .pipe(
        catchError(this.handleError)
      );
  }

  // Método para excluir uma rota existente
  deleteRoute(routeId: string): Observable<any> {
    const url = `${this.apiUrl}/${routeId}`;
    return this.http.delete<any>(url)
      .pipe(
        catchError(this.handleError)
      );
  }
}
