"use client";

import { useCreateTask } from "@/hooks/tasks";
import { Input } from "@/components/ui/input";
import { useCallback, useState } from "react";
import { Button } from "@/components/ui/button";

export function TaskComposer() {
  const { mutate: createTask } = useCreateTask();

  const [title, setTitle] = useState<string>();

  const onChange = useCallback((e: React.ChangeEvent<HTMLInputElement>) => {
    setTitle(e.target.value);
  }, []);

  const onClick = useCallback(() => {
    if (!title) return;

    createTask({ title });
    setTitle("");
  }, [createTask, title]);

  return (
    <div className="flex flex-row gap-2 p-4 border-b border-gray-200 w-full items-center justify-center">
      <Input
        value={title}
        onChange={onChange}
        className="flex-grow-1 bg-background"
      />
      <Button onClick={onClick}>Create</Button>
    </div>
  );
}
