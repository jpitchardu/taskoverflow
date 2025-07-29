"use client";

import { useCreateTask } from "@/hooks/tasks";
import { Input } from "@/components/ui/input";
import { useCallback, useState } from "react";
import { Button } from "@/components/ui/button";
import { cn } from "@/lib/utils";
import { Loader2 } from "lucide-react";

export function TaskComposer() {
  const { mutate: createTask, isPending } = useCreateTask();

  const [title, setTitle] = useState<string>();
  const [isValid, setIsValid] = useState<boolean>();

  const onChange = useCallback((e: React.ChangeEvent<HTMLInputElement>) => {
    setTitle(e.target.value);
    setIsValid(e.target.validity.valid);
  }, []);

  const onFocus = useCallback((e: React.FocusEvent<HTMLInputElement>) => {
    setIsValid(e.target.validity.valid);
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
        onFocus={onFocus}
        className={cn(
          "flex-grow-1",
          "bg-background",
          isValid === false && "border-destructive",
          isValid === true && "border-primary"
        )}
        minLength={1}
        maxLength={100}
        required
      />
      <Button onClick={onClick} disabled={!isValid || isPending}>
        {isPending ? <Loader2 className="w-4 h-4 animate-spin" /> : null}
        Create
      </Button>
    </div>
  );
}
