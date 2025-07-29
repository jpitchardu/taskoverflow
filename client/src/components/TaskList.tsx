"use client";

import { TaskComposer } from "@/components/TaskComposer";
import { TaskListDetail } from "@/components/TaskListDetail";
import { useTasks } from "@/hooks/tasks";

export function TaskList() {
  const { data: tasks, isPending } = useTasks();

  if (isPending) return <div>Loading...</div>;

  const sortedTasks = tasks?.sort((a, b) => {
    if (a.isCompleted && !b.isCompleted) return 1;
    if (!a.isCompleted && b.isCompleted) return -1;
    return 0;
  });

  return (
    <div className="flex flex-col gap-2 w-1/2 p-4 min-h-1/3 bg-muted rounded-xl shadow-md items-center justify-start">
      <TaskComposer />
      {sortedTasks?.length ? (
        sortedTasks.map((task) => (
          <TaskListDetail
            key={task.id}
            id={task.id}
            title={task.title}
            isCompleted={task.isCompleted}
          />
        ))
      ) : (
        <div className="text-center text-muted-foreground flex-grow-1 w-full flex items-center justify-center">
          <span className="text-xl">No tasks, create one!</span>
        </div>
      )}
    </div>
  );
}
