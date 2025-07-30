import { TaskList } from "@/components/TaskList";
import { MockAppContainer } from "@/testUtils/MockAppContainer";
import { render, waitFor } from "@testing-library/react";
import nock from "nock";
import { describe } from "node:test";
import { expect, it } from "vitest";

function renderComponent() {
  return render(
    <MockAppContainer>
      <TaskList />
    </MockAppContainer>
  );
}

describe("TaskList", () => {
  it("should display the tasks", async () => {
    nock("http://localhost:5000")
      .get("/api/task")
      .reply(200, [
        { id: "1", title: "Task 1" },
        { id: "2", title: "Task 2" },
      ]);

    const { getByText } = renderComponent();

    waitFor(() => {
      expect(getByText("Task 1")).toBeDefined();
      expect(getByText("Task 2")).toBeDefined();
    });
  });
});
