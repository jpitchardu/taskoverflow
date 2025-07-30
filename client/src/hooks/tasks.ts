import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

const API_URL = "/api";

type Task = {
  id: string;
  title: string;
  isCompleted: boolean;
  createdAt: string;
  updatedAt: string;
};

export interface CreateTaskPayload {
  title: string;
}

export interface CompleteTaskPayload {
  id: string;
  isCompleted: boolean;
}

export function useTasks() {
  return useQuery({
    queryKey: ["tasks"],
    queryFn: async () => {
      const response = await fetch(`${API_URL}/task`);
      const data = await response.json();

      return data.tasks as Task[];
    },
  });
}

export function useTask(id: string) {
  return useQuery({
    queryKey: ["tasks", id],
    queryFn: async () => {
      const response = await fetch(`${API_URL}/task/${id}`);
      const data = await response.json();
      return data.task as Task;
    },
  });
}

export function useCreateTask() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (task: CreateTaskPayload) => {
      const response = await fetch(`${API_URL}/task`, {
        method: "POST",
        body: JSON.stringify(task),
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (!response.ok) {
        throw new Error("Failed to create task");
      }

      const data = await response.json();
      return data.task as Task;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["tasks"] });
    },
  });
}

export function useCompleteTask() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (task: CompleteTaskPayload) => {
      const response = await fetch(`${API_URL}/task/${task.id}`, {
        method: "PATCH",
        body: JSON.stringify({ isCompleted: true }),
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (!response.ok) {
        throw new Error("Failed to update task");
      }

      return response.json();
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["tasks"] });
    },
  });
}

export function useDeleteTask() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (id: string) => {
      const response = await fetch(`${API_URL}/task/${id}`, {
        method: "DELETE",
      });

      if (!response.ok) {
        throw new Error("Failed to delete task");
      }
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["tasks"] });
    },
  });
}
