"use client";

import React from "react";
import { toast } from "sonner";

export class ErrorBoundary extends React.Component<
  { children: React.ReactNode },
  { hasError: boolean }
> {
  constructor(props: { children: React.ReactNode }) {
    super(props);
    this.state = { hasError: false };
  }

  static getDerivedStateFromError(error: Error) {
    return { hasError: true };
  }

  componentDidCatch(error: Error, errorInfo: React.ErrorInfo) {
    console.error(error, errorInfo);
    toast.error("An error occurred while fetching tasks", {
      description: error.message,
    });
  }

  render() {
    return this.props.children;
  }
}
