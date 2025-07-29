"use client";

import { useDeleteTask, useUpdateTask } from "@/hooks/tasks";

type TaskListDetailProps = {
  id: string;
  title: string;
};

export function TaskListDetail({ id, title }: TaskListDetailProps) {
  const { mutate: deleteTask } = useDeleteTask();
  const { mutate: updateTask } = useUpdateTask();

  return (
    <div className="flex flex-row gap-2 p-4 border-b border-gray-200">
      <h2>{title}</h2>
      <button onClick={() => deleteTask(id)}>Delete</button>
      <button onClick={() => updateTask({ id, title })}>Update</button>
    </div>
  );
}
