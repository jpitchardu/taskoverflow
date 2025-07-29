"use client";

import { Button } from "@/components/ui/button";
import { useDeleteTask, useCompleteTask } from "@/hooks/tasks";
import { cn } from "@/lib/utils";
import { useCallback } from "react";

type TaskListDetailProps = {
  id: string;
  title: string;
  isCompleted: boolean;
};

export function TaskListDetail({
  id,
  title,
  isCompleted,
}: TaskListDetailProps) {
  const { mutate: deleteTask } = useDeleteTask();
  const { mutate: completeTask } = useCompleteTask();

  const handleCompleteTask = useCallback(() => {
    completeTask({ id, isCompleted: true });
  }, [id, completeTask]);

  const handleDeleteTask = useCallback(() => {
    deleteTask(id);
  }, [id, deleteTask]);

  return (
    <div className="flex flex-row gap-2 p-4 border-b border-gray-200 w-full items-center justify-between">
      <h2 className={cn("text-lg", isCompleted && "line-through")}>{title}</h2>
      <div className="flex flex-row gap-2">
        <Button variant="destructive" onClick={handleDeleteTask}>
          ☠️
        </Button>
        {!isCompleted && (
          <Button variant="secondary" onClick={handleCompleteTask}>
            ✔️
          </Button>
        )}
      </div>
    </div>
  );
}
