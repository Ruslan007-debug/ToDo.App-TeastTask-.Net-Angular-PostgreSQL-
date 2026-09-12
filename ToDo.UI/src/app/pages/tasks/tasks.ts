import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TaskService } from '../../core/services/taskService';
import { CategeoryService } from '../../core/services/categoryService';
import { TaskItemModel } from '../../models/TaskItem/taskItemModel';
import { CategoryModel } from '../../models/Category/categoryModel';
import { finalize } from 'rxjs';

@Component({
    selector: 'app-tasks',
    imports: [FormsModule],
    templateUrl: './tasks.html',
    styleUrl: './tasks.css'
})
export class Tasks implements OnInit{  //зобов`язується реалізувати інтерфейс OnInit(Angular lifecycle interface)
    tasks: TaskItemModel[] = [];
    categories: CategoryModel[] = [];

    searchTerm = '';
    selectedCategoryId: number | null=null;

    page = 1;
    pageSize = 10;
    totalPages = 0;

    isLoading = false;
    errorMessage = '';

    constructor(
        private taskService: TaskService,
        private categoryService: CategeoryService,
        private cdr: ChangeDetectorRef
    ){}

    ngOnInit(): void {  //автоматичне завантаження данних після створення компонента
        this.loadCategories();
        this.loadTasks();
    }
    
    loadTasks(): void{
        this.isLoading = true;
        this.errorMessage = '';
        console.log('START loading:', this.isLoading);

        this.taskService.getAll(
            this.searchTerm,
            this.selectedCategoryId,
            this.page,
            this.pageSize
        ).pipe(
            finalize(()=>{
                this.isLoading = false;
                console.log('FINALIZE loading:', this.isLoading);
                this.cdr.detectChanges();
            })
        ).subscribe({
            next: response=>{
                console.log('RESPONSE:', response);
                this.tasks = response.items,
                this.page = response.page,
                this.totalPages = response.totalPages
            },
            error: () =>{
                console.log('ERROR:', Error);
                this.errorMessage = 'Failed to load Tasks';
            }
        });
    }

    loadCategories(): void{
        this.categoryService.getAll().subscribe({
            next: categories=>{
                this.categories = categories;
                this.cdr.detectChanges();
            },
            error: () =>{
                this.errorMessage = 'Failed to load Categories'
                this.cdr.detectChanges();
            }
        })
    }

    applyFilters(): void{
        this.page = 1;
        this.loadTasks();
    }

    previousPage(): void{
        if(this.page>1){
            this.page--;
            this.loadTasks();
        }
    }

    nextPage(): void{
        if(this.page<this.totalPages){
            this.page++;
            this.loadTasks();
        }
    }

    deleteTask(id: number): void{
        this.taskService.delete(id).subscribe({
            next: ()=>{
                this.loadTasks;
            },
            error: ()=>{
                this.errorMessage = 'Failed to delete task.'
            }
        })
    }

}