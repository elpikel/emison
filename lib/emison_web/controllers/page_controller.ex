defmodule EmisonWeb.PageController do
  use EmisonWeb, :controller

  def index(conn, _params) do
    render(conn, "index.html")
  end
end