defmodule Emison.Greeting do
  use Ecto.Schema
  import Ecto.Changeset

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  schema "greetings" do
    field :image, :string
    field :name, :string
    field :wedding_id, :binary_id

    timestamps()
  end

  @doc false
  def changeset(greeting, attrs) do
    greeting
    |> cast(attrs, [:name, :image])
    |> validate_required([:name, :image])
  end
end
