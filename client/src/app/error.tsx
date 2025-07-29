"use client";

import { Button } from "@/components/ui/button";
import { useEffect } from "react";

export default function Error({ error }: { error: Error }) {
  useEffect(() => {
    console.error(error);
  }, [error]);

  return (
    <div className="flex flex-col gap-2 items-center justify-center h-screen">
      <h1 className="text-2xl font-bold">Something went wrong</h1>
      <Button variant="outline" onClick={() => window.location.reload()}>
        Refresh the page
      </Button>
    </div>
  );
}
