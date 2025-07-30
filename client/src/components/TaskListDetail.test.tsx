import { TaskListDetail } from "@/components/TaskListDetail";
import { MockAppContainer } from "@/testUtils/MockAppContainer";
import {
  findAllByLabelText,
  fireEvent,
  getByLabelText,
  render,
  waitFor,
} from "@testing-library/react";
import nock from "nock";
import { describe } from "node:test";
import { expect, it } from "vitest";

function renderComponent(isCompleted: boolean) {
  return render(
    <MockAppContainer>
      <TaskListDetail id="1" title="Task 1" isCompleted={isCompleted} />
    </MockAppContainer>
  );
}

describe("TaskListDetail", () => {
  it("should display the task", async () => {
    const { getByText } = renderComponent(false);

    waitFor(() =>
      expect(getByText("Task 1").classList.contains("line-through")).toBe(false)
    );
  });

  it("should display the task as completed", async () => {
    const { getByText } = renderComponent(true);

    waitFor(() => {
      expect(getByText("Task 1").classList.contains("line-through")).toBe(true);
    });
  });

  it("should toggle the task", async () => {
    const scope = nock("http://localhost:8080")
      .patch("/api/task/1")
      .reply(200, { id: "1", title: "Task 1", isCompleted: true });

    const { getByText, findAllByLabelText } = renderComponent(false);

    const button = (
      await findAllByLabelText("Complete Task")
    )[0] as HTMLButtonElement;

    fireEvent.click(button);

    waitFor(() => {
      expect(scope.isDone()).toBe(true);
    });

    waitFor(() => {
      expect(getByText("Task 1").classList.contains("line-through")).toBe(true);
    });
  });

  it("should delete the task", async () => {
    const scope = nock("http://localhost:8080")
      .delete("/api/task/1")
      .reply(200, { id: "1", title: "Task 1", isCompleted: false });

    const { findAllByLabelText } = renderComponent(false);

    const button = (
      await findAllByLabelText("Delete Task")
    )[0] as HTMLButtonElement;

    fireEvent.click(button);

    waitFor(() => {
      expect(scope.isDone()).toBe(true);
    });
  });
});
