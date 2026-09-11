import { CategoryModel } from '../Category/categoryModel';

export interface TaskItemModel
{
    id: number;
    title: string;
    description: string | null;
    isCompleted: boolean;
    dueDate: string | null;
    createdAt: string;
    userId: number;
    categoryId: number | null;
    category: CategoryModel | null;
}