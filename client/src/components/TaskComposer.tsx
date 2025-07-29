"use client";

import { useCreateTask } from "@/hooks/tasks";
import { useCallback, useState } from "react";

export function TaskComposer() {
  const { mutate: createTask } = useCreateTask();

  const [title, setTitle] = useState("");

  const onChange = useCallback((e: React.ChangeEvent<HTMLInputElement>) => {
    setTitle(e.target.value);
  }, []);

  const onClick = useCallback(() => {
    createTask({ title });
    setTitle("");
  }, [createTask, title]);

  return (
    <div className="flex flex-row gap-2 p-4 border-b border-gray-200">
      <input type="text" value={title} onChange={onChange} />
      <button onClick={onClick}>Create</button>
    </div>
  );
}
