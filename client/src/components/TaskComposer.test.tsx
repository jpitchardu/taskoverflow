import { describe, expect, it } from "vitest";
import { fireEvent, render, waitFor } from "@testing-library/react";
import { TaskComposer } from "./TaskComposer";
import nock from "nock";
import { MockAppContainer } from "@/testUtils/MockAppContainer";

function renderComponent() {
  return render(
    <MockAppContainer>
      <TaskComposer />
    </MockAppContainer>
  );
}

describe("TaskComposer", () => {
  it("should render", async () => {
    const { getByText } = renderComponent();

    await waitFor(() => {
      expect(getByText("Create")).toBeDefined();
    });
  });

  it("should create a task", async () => {
    nock("http://localhost:5000")
      .post("/api/task")
      .reply(200, { id: "1", title: "Test Task" });

    const { findAllByText, findAllByPlaceholderText } = renderComponent();

    const button = (await findAllByText("Create"))[0] as HTMLButtonElement;

    expect(button).toBeDefined();
    expect(button.disabled).toBe(true);

    const input = (
      await findAllByPlaceholderText("Add a task")
    )[0] as HTMLInputElement;

    fireEvent.change(input, { target: { value: "Test Task" } });

    expect(button.disabled).toBe(false);

    fireEvent.click(button);
  });
});
