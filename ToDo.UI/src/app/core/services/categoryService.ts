import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { CategoryModel } from "../../models/Category/categoryModel";
import { CreateCategory } from "../../models/Category/createCategory";
import { UpdateCategory } from "../../models/Category/updateCategory";

@Injectable({
    providedIn: 'root'
})
export class CategeoryService{
    private readonly apiUrl = 'https://localhost:7165/api/categories';
    constructor(private http: HttpClient) {}

    getAll(): Observable<CategoryModel[]>{
        return this.http.get<CategoryModel[]>(this.apiUrl);
    }

    getById(id: number): Observable<CategoryModel>{
        return this.http.get<CategoryModel>(`${this.apiUrl}/${id}`);
    }

    create(request: CreateCategory): Observable<CategoryModel>{
        return this.http.post<CategoryModel>(this.apiUrl, request);
    }

    update(id: number, request: UpdateCategory): Observable<CategoryModel>{
        return this.http.put<CategoryModel>(`${this.apiUrl}/${id}`, request);
    }

    delete(id: number):Observable<CategoryModel>{
        return this.http.delete<CategoryModel>(`${this.apiUrl}/${id}`)
    }
}
