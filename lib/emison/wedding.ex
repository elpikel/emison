defmodule Emison.Wedding do
  use Ecto.Schema
  import Ecto.Changeset

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  schema "weddings" do
    field :description, :string
    field :name, :string
    has_many :greetings, Emison.Greeting

    timestamps()
  end

  @doc false
  def changeset(wedding, attrs) do
    wedding
    |> cast(attrs, [:name, :description])
    |> validate_required([:name, :description])
  end
end
