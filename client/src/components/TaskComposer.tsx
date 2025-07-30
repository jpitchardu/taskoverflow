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
  const [error, setError] = useState<string>();

  const onChange = useCallback((e: React.ChangeEvent<HTMLInputElement>) => {
    setTitle(e.target.value);
    setIsValid(e.target.validity.valid);
    setError(e.target.validationMessage);
  }, []);

  const onFocus = useCallback((e: React.FocusEvent<HTMLInputElement>) => {
    setIsValid(e.target.validity.valid);
  }, []);

  const onClick = useCallback(() => {
    if (!title) return;

    createTask({ title });
    setTitle("");
  }, [createTask, title]);

  // To ensure it is disabled  after re-rendering
  const canSubmit = isValid && title?.length && title.length > 0 && !isPending;

  return (
    <div className="flex flex-row gap-2 p-4 border-b border-gray-200 w-full items-start justify-center">
      <div className="flex flex-col gap-2">
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
          maxLength={20}
          required
        />
        {!isValid && error ? (
          <p className=" text-xs text-destructive">{error}</p>
        ) : null}
      </div>
      <Button onClick={onClick} disabled={!canSubmit}>
        {isPending ? <Loader2 className="w-4 h-4 animate-spin" /> : null}
        Create
      </Button>
    </div>
  );
}
