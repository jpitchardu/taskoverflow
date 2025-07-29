"use client";

import { TaskComposer } from "@/components/TaskComposer";
import { TaskListDetail } from "@/components/TaskListDetail";
import { useTasks } from "@/hooks/tasks";

export function TaskList() {
  const { data: tasks, isPending } = useTasks();

  if (isPending) return <div>Loading...</div>;

  return (
    <div className="flex flex-col gap-2">
      <TaskComposer />
      {tasks &&
        tasks.length > 0 &&
        tasks.map((task) => (
          <TaskListDetail key={task.id} id={task.id} title={task.title} />
        ))}
      {tasks && tasks.length === 0 && <div>No tasks</div>}
    </div>
  );
}
