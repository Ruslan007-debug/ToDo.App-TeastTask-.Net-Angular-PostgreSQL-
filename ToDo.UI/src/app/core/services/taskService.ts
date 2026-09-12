import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TaskItemModel } from '../../models/TaskItem/taskItemModel';
import { CreateTaskItem } from '../../models/TaskItem/createTaskItem';
import { UpdateTaskItem } from '../../models/TaskItem/updateTaskItem';
import { PagedResult } from '../../models/pagination/pagedResult';


@Injectable({
    providedIn: 'root'
})
export class TaskService{
    private readonly apiUrl = 'https://localhost:7165/api/taskitems';

    constructor(private http: HttpClient){}

    getAll(
        searchTerm: string = '',
        categoryId: number | null=null,
        page: number = 1,
        pageSize: number = 10
    ):Observable<PagedResult<TaskItemModel>>{
        let params = new HttpParams()
        .set('page', page)
        .set('pageSize', pageSize);

        if (searchTerm) {
            params = params.set('searchTerm', searchTerm);
        }

        if (categoryId !== null) {
            params = params.set('categoryId', categoryId);
        }

        return this.http.get<PagedResult<TaskItemModel>>(this.apiUrl, {params});
    }

    getById(id: number): Observable<TaskItemModel>{
        return this.http.get<TaskItemModel>(`${this.apiUrl}/${id}`);
    }

    create(request: CreateTaskItem): Observable<TaskItemModel>{
        return this.http.post<TaskItemModel>(this.apiUrl, request);
    }

    update(id: number, request: UpdateTaskItem): Observable<TaskItemModel>{
        return this.http.put<TaskItemModel>(`${this.apiUrl}/${id}`, request);
    }

    delete(id: number):Observable<TaskItemModel>{
        return this.http.delete<TaskItemModel>(`${this.apiUrl}/${id}`);
    }
}