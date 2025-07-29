"use client";

import { TaskComposer } from "@/components/TaskComposer";
import { TaskListDetail } from "@/components/TaskListDetail";
import { Skeleton } from "@/components/ui/skeleton";
import { useTasks } from "@/hooks/tasks";
import { ScrollArea } from "@radix-ui/react-scroll-area";

export function TaskList() {
  const { data: tasks, isPending, isLoading } = useTasks();

  if (isPending || isLoading)
    return (
      <div className="flex flex-col gap-2 w-1/2 p-4 min-h-1/3 bg-card rounded-xl shadow-md items-center justify-start space-y-4">
        <Skeleton className="w-full h-10 bg-accent" />
        <Skeleton className="w-full h-10 bg-accent" />
        <Skeleton className="w-full h-10 bg-accent" />
      </div>
    );

  const sortedTasks = tasks?.sort((a, b) => {
    if (a.isCompleted && !b.isCompleted) return 1;
    if (!a.isCompleted && b.isCompleted) return -1;
    return 0;
  });

  return (
    <div className="flex flex-col gap-2 w-1/2 p-4 max-h-9/10 overflow-hidden min-h-9/10 bg-card rounded-xl shadow-md items-center justify-start">
      <TaskComposer />
      <ScrollArea className="min-h-0 w-full flex flex-col gap-2 items-center justify-start overflow-y-auto">
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
      </ScrollArea>
    </div>
  );
}
