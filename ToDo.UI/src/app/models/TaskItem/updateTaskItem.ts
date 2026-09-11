export interface UpdateTaskItem {
  title: string;
  description: string | null;
  isCompleted: boolean;
  dueDate: string | null;
  categoryId: number | null;
}