"use client";

import { ErrorBoundary } from "@/providers/ErrorBoundary";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

const queryClient = new QueryClient();

export function Providers({ children }: { children: React.ReactNode }) {
  return (
    <ErrorBoundary>
      <QueryClientProvider client={queryClient}>{children}</QueryClientProvider>
    </ErrorBoundary>
  );
}
