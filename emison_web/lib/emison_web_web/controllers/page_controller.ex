defmodule EmisonWebWeb.PageController do
  use EmisonWebWeb, :controller

  def index(conn, _params) do
    render(conn, "index.html")
  end
end
