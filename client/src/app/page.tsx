import { TaskList } from "@/components/TaskList";

export default function Home() {
  return (
    <div className="flex flex-col gap-2 items-center justify-center h-screen">
      <TaskList />
    </div>
  );
}
